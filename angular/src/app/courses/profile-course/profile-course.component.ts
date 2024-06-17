import { ActivatedRoute, Router } from '@angular/router';
import { ICourseByID } from '@shared/interfaces/CourseDto/course.getId.interface';
import { ServicesCourseService } from '@shared/services/services.Course.service';
import { IUserByIDResult } from '../../../shared/interfaces/UserDto/user.getId.view.interface'
import { UserService } from '../../../shared/services/user.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Component, Injector, OnInit, inject } from '@angular/core';
import { CreateCourseDialogComponent } from '../create-course/create-course.component';
import { EditCourseComponent } from '../edit-course/edit-course.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';

@Component({
  selector: 'app-profile-course',
  templateUrl: './profile-course.component.html',
  animations: [appModuleAnimation()]
})
export class ProfileCourseComponent extends AppComponentBase implements OnInit {
  public idCourse: string;

  course: ICourseByID = {
    result: {
      name: '',
      description: '',
      duration: 0,
      maxStudents: 0,
      minStudents: 0,
      userTeacherId: 0,
      id: 0
    },
    targetUrl: null,
    success: false,
    error: null,
    unAuthorizedRequest: false,
    __abp: false
  };
  user: IUserByIDResult = {
    result: {
      userName: '',
      name: '',
      surname: '',
      emailAddress: '',
      isActive: false,
      fullName: '',
      lastLoginTime: null,
      creationTime: new Date(),
      roleNames: [''],
      id: 0
    },
    targetUrl: null,
    success: false,
    error: null,
    unAuthorizedRequest: false,
    __abp: false
  };
  currentTab: string = 'estudiantesTab';
  public serviceCourse = inject(ServicesCourseService);
  public serviceUser = inject(UserService);

  constructor(
    injector: Injector,
    private _modalService: BsModalService,
    private route: ActivatedRoute,
    private router: Router
  )
  {
    super(injector);
  }

  async ngOnInit(): Promise<void> {
    this.route.paramMap.subscribe(params => {
      this.idCourse = params.get('id');
    });

    if (this.idCourse) {
      await this.loadCourse(this.idCourse);
      if (this.course && this.course.result && this.course.result.userTeacherId) {
        await this.loadUser(this.course.result.userTeacherId.toString());
      }
    }
  }

  loadCourse(id: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.serviceCourse.getCourseById(id).subscribe(
        result => {
          this.course = result;
          resolve();
        },
        error => {
          console.error('Error fetching courses:', error);
          reject(error);
        }
      );
    });
  }

  loadUser(id: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.serviceUser.getUserById(id).subscribe(
        result => {
          this.user = result;
          resolve();
        },
        error => {
          console.error('Error fetching user:', error);
          reject(error);
        }
      );
    });
  }

  selectTab(tab: string) {
    this.currentTab = tab;
  }

  editCourse(): void {
    this.showEditCoursesDialog(this.course.result.id);
  }

  private showEditCoursesDialog(id: number): void {
    let createOrEditCourseDialog: BsModalRef;
    if (id) {
      createOrEditCourseDialog = this._modalService.show(
        EditCourseComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }
    createOrEditCourseDialog.content.onSave.subscribe(() => {
      this.loadCourse(this.idCourse);
    });
  }

  protected delete(): void {
    abp.message.confirm(
      this.l('deleteMessage', this.course.result.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this.serviceCourse.deleteCourse(this.course.result.id.toString()).subscribe(() => {
            abp.notify.success(this.l('SuccessfullyDeleted'));
            this.refresh();
          });
        }
      }
    );
  }

  private refresh(): void {
    this.router.navigate(['app/courses']);
  }
}
