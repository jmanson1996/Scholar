import { Component, Injector, OnInit, inject } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
  UserServiceProxy,
} from '@shared/service-proxies/service-proxies';
import { CreateCourseDialogComponent } from './create-course/create-course.component';
import { ServicesCourseService } from '@shared/services/services.Course.service';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  animations: [appModuleAnimation()]
})
export class CoursesComponent implements OnInit {

  courses: any[] = [];
  public totalCourses = 0;
  pageSize = 2;
  currentPage = 1;
  idUser : number;

  columns = [
    { field: 'name', header: 'Name' },
    { field: 'description', header: 'Description' },
    { field: 'id', header: 'ID' }
  ]

  public service = inject(ServicesCourseService);
  constructor(
    injector: Injector,
    private _userService: UserServiceProxy,
    private _modalService: BsModalService
  )
  {
  }


  ngOnInit(): void {
    this.loadCourses(this.currentPage);
  }

  loadCourses(page: number): void {
    const skipCount = (page - 1) * this.pageSize;
    this.idUser = Number(localStorage.getItem('userId'));
    console.log('user',this.idUser);
    this.service.getAllByUser(this.idUser, skipCount, this.pageSize).subscribe(
      result => {
        this.courses = result.result.items;
        this.totalCourses = result.result.totalCount;
        this.currentPage = page;
      },
      error => {
        console.error('Error fetching courses:', error);
      }
    );
  }

  createUser(): void {
    this.showCreateOrEditCoursesDialog();
  }

  private showCreateOrEditCoursesDialog(id?: number): void {
    let createOrEditCourseDialog: BsModalRef;
    if (!id) {
      createOrEditCourseDialog = this._modalService.show(
        CreateCourseDialogComponent,
        {
          class: 'modal-lg',
        }
      );
    }
    createOrEditCourseDialog.content.onSave.subscribe(() => {
      this.loadCourses(this.currentPage);
    });
  }
}
