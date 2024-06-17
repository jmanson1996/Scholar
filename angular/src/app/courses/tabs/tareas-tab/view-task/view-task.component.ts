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
import { UserService } from '@shared/services/user.service';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { IResponseCourseNotFilterFullView, Item as itemNotFilter} from '@shared/interfaces/UserDto/courseStudentByCourseNotFilter.ViewFull.interface';
import { ServicesCourseService } from '@shared/services/services.Course.service';
import { ITaskByIDView, Result } from '@shared/interfaces/CourseDto/course.task.view.byId.interface';
import { IResponseTaskByCourse, Item } from '@shared/interfaces/UserDto/courseStudentTaskByCourse.view';

@Component({
  selector: 'app-view-task',
  templateUrl: './view-task.component.html',
  styleUrl: './view-task.component.css',
  animations: [appModuleAnimation()]
})
export class ViewTaskComponent extends AppComponentBase
implements OnInit {
  taskView: Result = {
    title: '',
    description: '',
    deliveryDate: new Date(),
    courseId: 0,
    id: 0
  };

  taskStudent: Item[] = [];

  idTask: number;
  idCourse : string;
  data: Date;
  saving = false;
  dateCurrent : string = this.setDate();
  @Output() onSave = new EventEmitter<any>();
  public service = inject(UserService);
  public serviceCourse = inject(ServicesCourseService);

  constructor(
    injector: Injector,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  async ngOnInit(): Promise<void> {
    this.getTaskById(this.idTask.toString());
    console.log(this.taskView);
    this.getStudentPresentTask(this.idCourse, this.idTask.toString());
  }


  getTaskById(idCourse : string): void {
    this.service
    .getInfoTaskById(
      idCourse
    )
    .subscribe((result: ITaskByIDView) => {
      this.taskView = result.result;
    });
  }

  getStudentPresentTask(idCourse : string, idTask : string): void {
    this.service
    .getStudentPresentTask(
      idCourse,
      idTask
    )
    .subscribe((result: IResponseTaskByCourse) => {
      this.taskStudent = result.result.items;
    });
  }

  save(): void {
    console.log(this.taskStudent);
    this.saving = true;
    this.serviceCourse.createOrUpdateAssistance(this.taskStudent).subscribe(
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

  setDate() : string{
      const dateCurrent = new Date();
      const day = dateCurrent.getDate();
      const month = dateCurrent.getMonth() + 1;
      const year = dateCurrent.getFullYear();
      const dateFormatted = `${day}/${month}/${year}`;
      return dateFormatted;
  }
}
