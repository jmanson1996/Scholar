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
import { CreateStudentAssistenceComponent } from './create-student/create-student.component';
import { IResponseAssistanceViewTable, Item, Result } from '@shared/interfaces/CourseDto/course.assistance.viewTable.interface';
import { ViewAssistanceComponent } from './view-assistance/view-assistance.component';

class PagedUsersRequestDto extends PagedRequestDto {
  keyword: string;
  isActive: boolean | null;
}

@Component({
  selector: 'app-asistencias-tab',
  templateUrl: './asistencias-tab.component.html',
  animations: [appModuleAnimation()]
})

export class AsistenciasTabComponent extends PagedListingComponentBase<Item>{

  users: Item[] = [];
  keyword = '';
  isActive: boolean | null;
  advancedFiltersVisible = false;
  @Input() idCourse: string;
  public service = inject(UserService);
  paged : PagedResultDto = {
    items: [],
    totalCount: 0
  };

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

  public resetPassword(user: UserDto): void {
    this.showResetPasswordUserDialog(user.id);
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
      .getViewAssistance(
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
      .subscribe((result: IResponseAssistanceViewTable) => {
        this.users = result.result.items;
        this.paged.items = result.result.items;
        this.paged.totalCount = result.result.items.length;
        this.showPaging(this.paged, pageNumber);
      });
  }

  protected visualise(user: Item): void {
    let createOrEditUserDialog: BsModalRef;
    createOrEditUserDialog = this._modalService.show(ViewAssistanceComponent, {
      class: 'modal-lg',
      initialState: {
        idCourse: Number(this.idCourse),
        data : user.date
      },
    });
    createOrEditUserDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }

  protected delete(entity: Item): void {
    throw new Error('Method not implemented.');
  }

  private showResetPasswordUserDialog(id?: number): void {
    this._modalService.show(ResetPasswordDialogComponent, {
      class: 'modal-lg',
      initialState: {
        id: id,
      },
    });
  }

  private showCreateOrEditUserDialog(id?: number): void {
    let createOrEditUserDialog: BsModalRef;
    if (!id) {
      createOrEditUserDialog = this._modalService.show(
        CreateStudentAssistenceComponent,
        {
          class: 'modal-lg',
          initialState: {
            idCourse: Number(this.idCourse),
          },
        }
      );
    } else {
      createOrEditUserDialog = this._modalService.show(
        EditUserDialogComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }

    createOrEditUserDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }
}
