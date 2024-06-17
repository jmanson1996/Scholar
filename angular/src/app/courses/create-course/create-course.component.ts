import {
  Component,
  Injector,
  OnInit,
  inject,
  EventEmitter,
  Output
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
import { ICourse } from '../../../shared/interfaces/CourseDto/course.interface';
import { ServicesCourseService } from '../../../shared/services/services.Course.service';


@Component({
  selector: 'app-create-course',
  templateUrl: './create-course.component.html',
})
export class CreateCourseDialogComponent extends AppComponentBase
implements OnInit {
  saving = false;

  course : ICourse = {
    name: '',
    description: '',
    duration: 0,
    maxStudents: 0,
    minStudents: 0,
    userTeacherId: 0,
    id : null
  };

  @Output() onSave = new EventEmitter<any>();
  public service = inject(ServicesCourseService);
  constructor(
    injector: Injector,
    public _userService: UserServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
  }

  save(): void {
    this.course.userTeacherId = parseInt(localStorage.getItem('userId'));
    this.service.createCourse(this.course).subscribe(
      response => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      },
      error => {
        this.saving = false;
      }
    );
  }
}
