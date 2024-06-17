import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
  inject,
  ViewEncapsulation
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
import { ICourseTaskFull } from '@shared/interfaces/CourseDto/course.task.full.interface';
import { ServicesCourseService } from '@shared/services/services.Course.service';

@Component({
  selector: 'app-create-tasks',
  templateUrl: './create-tasks.component.html',
  styleUrls: ['./style.css'],
  animations: [appModuleAnimation()],
  encapsulation: ViewEncapsulation.Emulated // Utiliza Shadow DOM para encapsular estilos
})
export class CreateTasksComponent extends AppComponentBase
implements OnInit {

  create : ICourseTaskFull = {
    id: 0,
    title: '',
    description: '',
    deliveryDate: new Date(),
    courseId: 0
  }
  idCourse: number;

  saving = false;
  @Output() onSave = new EventEmitter<any>();
  public service = inject(ServicesCourseService);

  constructor(
    injector: Injector,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
  }


  save(): void {
    this.create.courseId = this.idCourse;
    console.log(this.create)
    this.saving = true;
    this.service.createCourseTaskAssistance(this.create).subscribe(
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
  }
}
