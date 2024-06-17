import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
  inject
} from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { forEach as _forEach, map as _map } from 'lodash-es';
import { AppComponentBase } from '@shared/app-component-base';
import {
  UserServiceProxy,
  CreateUserDto,
  RoleDto
} from '@shared/service-proxies/service-proxies';
import { AbpValidationError } from '@shared/components/validation/abp-validation.api';
import { UserService } from '@shared/services/user.service';
import { IStudentUserResponse } from '@shared/interfaces/UserDto/user.student.view.interface';
import { IUserCourseFull } from '@shared/interfaces/UserDto/userCourse.full.interface';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-create-student',
  templateUrl: './create-student.component.html',
  animations: [appModuleAnimation()]
})
export class CreateStudentComponent extends AppComponentBase
implements OnInit {
  student : IStudentUserResponse = {
    result: [],
    targetUrl: null,
    success: false,
    error: null,
    unAuthorizedRequest: false,
    __abp: false
  };

  create : IUserCourseFull = {
    idUser: 0,
    idCourse: 0
  }
  idCourse: number;

  saving = false;
  @Output() onSave = new EventEmitter<any>();
  public service = inject(UserService);

  constructor(
    injector: Injector,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.service.getStudentByRole(4).subscribe((result) => {
      this.student = result;
    });
  }


  save(): void {
    if(this.create.idUser !== 0){
      this.saving = true;
      this.create.idCourse = this.idCourse;
      this.service.createStudent(this.create).subscribe(
        () => {
          this.notify.info(this.l('SavedSuccessfully'));
          this.bsModalRef.hide();
          this.onSave.emit();
        },
        (error) => {
          this.saving = false;
          this.notify.error(error.error.error.validationErrors[0].message,'Error');
        }
      );
      return;
    }
  }
}
