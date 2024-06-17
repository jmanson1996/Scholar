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
import { CreateStudentComponent } from './create-student/create-student.component';
import { IResponseCourseUserByCourse, Item, Result } from '@shared/interfaces/UserDto/courseStudentByCourse.View.interface';
import { UserService } from '@shared/services/user.service';

class PagedUsersRequestDto extends PagedRequestDto {
  keyword: string;
  isActive: boolean | null;
}

@Component({
  selector: 'app-estudiantes-tab',
  templateUrl: './estudiantes-tab.component.html',
  animations: [appModuleAnimation()]
})
export class EstudiantesTabComponent extends PagedListingComponentBase<Item> {
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
      .getUserCourseByCourse(
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
      .subscribe((result: IResponseCourseUserByCourse) => {
        this.users = result.result.items;
        this.paged.items = result.result.items;
        this.paged.totalCount = result.result.items.length;
        this.showPaging(this.paged, pageNumber);
      });
  }

  protected delete(user: Item): void {
    abp.message.confirm(
      this.l('UserDeleteWarningMessage', user.fullName),
      undefined,
      (result: boolean) => {
        if (result) {
          this.service.deleteStudentCourse(user.id.toString(), this.idCourse).subscribe(() => {
            abp.notify.success(this.l('SuccessfullyDeleted'));
            this.refresh();
          });
        }
      }
    );
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
        CreateStudentComponent,
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
