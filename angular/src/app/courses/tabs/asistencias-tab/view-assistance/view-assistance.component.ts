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
@Component({
  selector: 'app-view-assistance',
  templateUrl: './view-assistance.component.html',
  animations: [appModuleAnimation()]
})
export class ViewAssistanceComponent  extends AppComponentBase
implements OnInit{
  studentsCreate: itemNotFilter[] = [];
  idCourse: number;
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

  ngOnInit(): void {
    this.getStudentByCourse(this.idCourse.toString(), this.data);
  }


  getStudentByCourse(idCourse : string, data : Date): void {
    this.service
      .getStudentCourseAssistence(
        idCourse,
        data
      )
      .subscribe((result: IResponseCourseNotFilterFullView) => {
        this.studentsCreate = result.result.items;
      });
  }

  save(): void {
    this.saving = true;
    this.serviceCourse.editCourseAssistance(this.studentsCreate).subscribe(
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
