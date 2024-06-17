import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
  inject
} from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { forEach as _forEach, includes as _includes, map as _map } from 'lodash-es';
import { AppComponentBase } from '@shared/app-component-base';
import {
  UserServiceProxy,
  UserDto,
  RoleDto
} from '@shared/service-proxies/service-proxies';
import { ICourse } from '@shared/interfaces/CourseDto/course.interface';
import { ServicesCourseService } from '@shared/services/services.Course.service';

@Component({
  selector: 'app-edit-course',
  templateUrl: './edit-course.component.html',
})
export class EditCourseComponent extends AppComponentBase
implements OnInit{
  saving = false;
  course : ICourse = {
    name: '',
    description: '',
    duration: 0,
    maxStudents: 0,
    minStudents: 0,
    userTeacherId: 0,
    id : 0
  };

  @Output() onSave = new EventEmitter<any>();
  id: number;

  public serviceCourse = inject(ServicesCourseService);

  constructor(
    injector: Injector,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  async ngOnInit(): Promise<void> {
    await this.loadCourse(this.id.toString());
  }

  loadCourse(id: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.serviceCourse.getCourseById(id).subscribe(
        result => {
          this.course.description = result.result.description;
          this.course.duration = result.result.duration;
          this.course.maxStudents = result.result.maxStudents;
          this.course.minStudents = result.result.minStudents;
          this.course.name = result.result.name;
          this.course.userTeacherId = result.result.userTeacherId;
          this.course.id = result.result.id;
          resolve();
        },
        error => {
          console.error('Error fetching courses:', error);
          reject(error);
        }
      );
    });
  }

  save(): void {
    this.saving = true;
    this.serviceCourse.editCourse(this.course).subscribe(
      () => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      },
      () => {
        this.saving = false;
      }
    );
  }
}
