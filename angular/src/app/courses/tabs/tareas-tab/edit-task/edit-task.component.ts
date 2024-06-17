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
import { ICourseTaskView } from '@shared/interfaces/CourseDto/course.task.view';
import { firstValueFrom, lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-edit-task',
  templateUrl: './edit-task.component.html',
  styleUrl: './edit-task.component.css'
})
export class EditTaskComponent extends AppComponentBase
implements OnInit{
  create : ICourseTaskFull = {
    id: 0,
    title: '',
    description: '',
    deliveryDate: new Date(),
    courseId: 0
  }
  idTask: number;

  saving = false;
  @Output() onSave = new EventEmitter<any>();
  public service = inject(ServicesCourseService);

  constructor(
    injector: Injector,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  async ngOnInit(): Promise<void> {
    console.log(this.idTask);
    try {
      const result = await firstValueFrom(this.service.getAsignationById(this.idTask.toString()));
      this.create.id = result.result.id;
      this.create.title = result.result.title;
      this.create.description = result.result.description;
      this.create.deliveryDate = result.result.deliveryDate;
      this.create.courseId = result.result.courseId;
      console.log(result);
    } catch (error) {
      console.error('Error fetching task assignment', error);
    }
  }
  save(): void {
    this.saving = true;
    this.service.editCourseTaskAssistance(this.create).subscribe(
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
