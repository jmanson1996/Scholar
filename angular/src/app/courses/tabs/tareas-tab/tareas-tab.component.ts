import { Component, Injector, Input, inject } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
  PagedListingComponentBase,
  PagedRequestDto,
  PagedResultDto
} from 'shared/paged-listing-component-base';
import {
  UserServiceProxy,
  UserDto,
  UserDtoPagedResultDto
} from '@shared/service-proxies/service-proxies';
import { ResetPasswordDialogComponent } from '@app/users/reset-password/reset-password.component';
import { CreateUserDialogComponent } from '@app/users/create-user/create-user-dialog.component';
import { EditUserDialogComponent } from '@app/users/edit-user/edit-user-dialog.component';
import { UserService } from '@shared/services/user.service';
import { CreateStudentComponent } from '../estudiantes-tab/create-student/create-student.component';
import { CreateTasksComponent } from './create-tasks/create-tasks.component';
import { ITaskView, Item } from '@shared/interfaces/CourseDto/course.task.view.interface';
import { ViewTaskComponent } from './view-task/view-task.component';
import { ServicesCourseService } from '@shared/services/services.Course.service';
import { EditTaskComponent } from './edit-task/edit-task.component';

class PagedUsersRequestDto extends PagedRequestDto {
  keyword: string;
  isActive: boolean | null;
}
@Component({
  selector: 'app-tareas-tab',
  templateUrl: './tareas-tab.component.html',
  styleUrls: ['./styleTareas.css'],
  animations: [appModuleAnimation()]
})
export class TareasTabComponent extends PagedListingComponentBase<Item>{
  users: Item[] = [];
  keyword = '';
  isActive: boolean | null;
  advancedFiltersVisible = false;
  @Input() idCourse: string;
  public service = inject(UserService);
  public serviceCourse = inject(ServicesCourseService);

  paged : PagedResultDto = {
    items: [],
    totalCount: 0
  };

  idTask: number;

  constructor(
    injector: Injector,
    private _userService: UserServiceProxy,
    private _modalService: BsModalService
  ) {
    super(injector);
  }

  createUserStudent(): void {
    this.showCreateOrEditUserDialog();
  }

  editUser(user: UserDto): void {
    this.showCreateOrEditUserDialog(user.id);
  }

  clearFilters(): void {
    this.keyword = '';
    this.isActive = undefined;
    this.getDataPage(1);
  }

  protected list(
    request: PagedUsersRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;
    request.isActive = this.isActive;

    this.service
      .getAllTask(
        request.keyword,
        request.isActive,
        request.skipCount,
        request.maxResultCount,
        this.idCourse
      )
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: ITaskView) => {
        this.users = result.result.items;
        this.paged.items = result.result.items;
        this.paged.totalCount = result.result.items.length;
        this.showPaging(this.paged, pageNumber);
      });
  }

  protected visualize(task : Item) : void {
    let createOrEditUserDialog: BsModalRef;
    createOrEditUserDialog = this._modalService.show(ViewTaskComponent, {
      class: 'modal-lg',
      initialState: {
        idTask: Number(task.id),
        idCourse : this.idCourse
        // data : user.date
      },
    });
    createOrEditUserDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }

  protected delete(user: Item): void {
    abp.message.confirm(
      this.l('DeleteWarningMessage', user.title),
      undefined,
      (result: boolean) => {
        if (result) {
          this.serviceCourse.deleteStudentPresentTask(user.id.toString()).subscribe(() => {
            abp.notify.success(this.l('SuccessfullyDeleted'));
            this.refresh();
          });
        }
      }
    );
  }

  private showCreateOrEditUserDialog(id?: number): void {
    let createOrEditTaskDialog: BsModalRef;
    if (!id) {
      createOrEditTaskDialog = this._modalService.show(
        CreateTasksComponent,
        {
          class: 'modal-lg',
          initialState: {
            idCourse: Number(this.idCourse),
          },
        }
      );
    }
    else {
      createOrEditTaskDialog = this._modalService.show(
        EditTaskComponent,
        {
          class: 'modal-lg',
          initialState: {
            idTask: Number(id),
          },
        }
      );
    }

    createOrEditTaskDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }
}
