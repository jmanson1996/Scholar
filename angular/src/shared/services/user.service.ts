import { ICourse, IResponseCourse } from '../interfaces/CourseDto/course.interface';
import { HttpClient, HttpHeaders, HttpParams  } from '@angular/common/http';
import { Injectable, computed, inject, signal } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ICourseByID } from '../interfaces/CourseDto/course.getId.interface';
import { IUserByIDResult } from '../interfaces/UserDto/user.getId.view.interface';
import { IStudentUserResponse } from '../interfaces/UserDto/user.student.view.interface';
import { IUserCourseFull } from '@shared/interfaces/UserDto/userCourse.full.interface';
import { IResponseCourseUserByCourse } from '@shared/interfaces/UserDto/courseStudentByCourse.View.interface';
import { IResponseCourseNotFilterFullView } from '@shared/interfaces/UserDto/courseStudentByCourseNotFilter.ViewFull.interface';
import { IResponseAssistanceViewTable } from '@shared/interfaces/CourseDto/course.assistance.viewTable.interface';
import { ITaskView } from '@shared/interfaces/CourseDto/course.task.view.interface';
import { ITaskByIDView } from '@shared/interfaces/CourseDto/course.task.view.byId.interface';
import {IResponseTaskByCourse } from '@shared/interfaces/UserDto/courseStudentTaskByCourse.view';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private http = inject(HttpClient);

  private headers = new HttpHeaders({
    'Authorization': 'Bearer '+ localStorage.getItem('accessToken'),
  })

  getUserById(id: string): Observable<IUserByIDResult> {
    const params = new HttpParams()
      .set('Id', id.toString());
    return this.http.get<IUserByIDResult>(environment.remoteServiceBaseUrl+'User/Get', { headers: this.headers, params: params});
  }

  getStudentByRole(id : number): Observable<IStudentUserResponse> {
    const params = new HttpParams()
      .set('idRole', id.toString());
    return this.http.get<IStudentUserResponse>(environment.remoteServiceBaseUrl+'User/GetUsersRoles', { headers: this.headers, params: params});
  }

  createStudent(student : IUserCourseFull): Observable<IUserCourseFull> {
    return this.http.post<IUserCourseFull>(environment.remoteServiceBaseUrl+'UserCourse/Create', student, { headers: this.headers });
  }

  getUserCourseByCourse(keyword: string | undefined, isActive: boolean | undefined, skipCount: number | undefined, maxResultCount: number | undefined,id: string | undefined): Observable<IResponseCourseUserByCourse> {
    let url_ = environment.remoteServiceBaseUrl + "User/getStudentCourse?";
    if (keyword === null)
        throw new Error("The parameter 'keyword' cannot be null.");
    else if (keyword !== undefined)
        url_ += "Keyword=" + encodeURIComponent("" + keyword) + "&";
    if (isActive === null)
        throw new Error("The parameter 'isActive' cannot be null.");
    else if (isActive !== undefined)
        url_ += "IsActive=" + encodeURIComponent("" + isActive) + "&";
    if (skipCount === null)
        throw new Error("The parameter 'skipCount' cannot be null.");
    else if (skipCount !== undefined)
        url_ += "SkipCount=" + encodeURIComponent("" + skipCount) + "&";
    if (maxResultCount === null)
        throw new Error("The parameter 'maxResultCount' cannot be null.");
    else if (maxResultCount !== undefined)
        url_ += "MaxResultCount=" + encodeURIComponent("" + maxResultCount) + "&";
    if (id === null)
      throw new Error("The parameter 'id' cannot be null.");
    else if (id !== undefined)
      url_ += "idCourse=" + encodeURIComponent("" + id) + "&";
    url_ = url_.replace(/[?&]$/, "");
    return this.http.get<IResponseCourseUserByCourse>(url_, { headers: this.headers});
  }

  getStudentCourseNotFilter(id: string): Observable<IResponseCourseNotFilterFullView> {
    const params = new HttpParams()
      .set('idCourse', id.toString());
    return this.http.get<IResponseCourseNotFilterFullView>(environment.remoteServiceBaseUrl+'User/getStudentCourseNotFilter', { headers: this.headers, params: params});
  }

  deleteStudentCourse(idUser : string, courseId :string) : Observable<any> {
    const params = new HttpParams()
      .set('idUser', idUser)
      .set('courseId', courseId);
    return this.http.delete<any>(environment.remoteServiceBaseUrl+'UserCourse/DeleteCourseStudentByPk', { headers: this.headers, params: params});
  }

  getViewAssistance(keyword: string | undefined, isActive: boolean | undefined, skipCount: number | undefined, maxResultCount: number | undefined,id: string): Observable<IResponseAssistanceViewTable> {
    let url_ = environment.remoteServiceBaseUrl + "Assistance/getViewAssistance?";
    if (keyword === null)
        throw new Error("The parameter 'keyword' cannot be null.");
    else if (keyword !== undefined)
        url_ += "Keyword=" + encodeURIComponent("" + keyword) + "&";
    if (isActive === null)
        throw new Error("The parameter 'isActive' cannot be null.");
    else if (isActive !== undefined)
        url_ += "IsActive=" + encodeURIComponent("" + isActive) + "&";
    if (skipCount === null)
        throw new Error("The parameter 'skipCount' cannot be null.");
    else if (skipCount !== undefined)
        url_ += "SkipCount=" + encodeURIComponent("" + skipCount) + "&";
    if (maxResultCount === null)
        throw new Error("The parameter 'maxResultCount' cannot be null.");
    else if (maxResultCount !== undefined)
        url_ += "MaxResultCount=" + encodeURIComponent("" + maxResultCount) + "&";
    url_ = url_.replace(/[?&]$/, "");

    const params = new HttpParams()
      .set('CourseId', id.toString());
    return this.http.get<IResponseAssistanceViewTable>(url_, { headers: this.headers, params: params});
  }

  getStudentCourseAssistence(id: string, data : Date): Observable<IResponseCourseNotFilterFullView> {
    const params = new HttpParams()
      .set('date', String(data))
      .set('idCourse', id.toString());
    return this.http.get<IResponseCourseNotFilterFullView>(environment.remoteServiceBaseUrl+'User/getStudentCourseAssistence', { headers: this.headers, params: params});
  }

  getAllTask(keyword: string | undefined, isActive: boolean | undefined, skipCount: number | undefined, maxResultCount: number | undefined,id: string): Observable<ITaskView> {
    let url_ = environment.remoteServiceBaseUrl + "Asignation/GetAll?";
    if (keyword === null)
        throw new Error("The parameter 'keyword' cannot be null.");
    else if (keyword !== undefined)
        url_ += "Keyword=" + encodeURIComponent("" + keyword) + "&";
    if (isActive === null)
        throw new Error("The parameter 'isActive' cannot be null.");
    else if (isActive !== undefined)
        url_ += "IsActive=" + encodeURIComponent("" + isActive) + "&";
    if (skipCount === null)
        throw new Error("The parameter 'skipCount' cannot be null.");
    else if (skipCount !== undefined)
        url_ += "SkipCount=" + encodeURIComponent("" + skipCount) + "&";
    if (maxResultCount === null)
        throw new Error("The parameter 'maxResultCount' cannot be null.");
    else if (maxResultCount !== undefined)
        url_ += "MaxResultCount=" + encodeURIComponent("" + maxResultCount) + "&";
    url_ = url_.replace(/[?&]$/, "");

    const params = new HttpParams()
      .set('CourseId', id.toString());
    return this.http.get<ITaskView>(url_, { headers: this.headers, params: params});
  }

  getInfoTaskById(id: string): Observable<ITaskByIDView> {
    const params = new HttpParams()
      .set('Id', id.toString());
    return this.http.get<ITaskByIDView>(environment.remoteServiceBaseUrl+'Asignation/Get', { headers: this.headers, params: params});
  }

  getStudentPresentTask(idCourse: string, idTask : string): Observable<IResponseTaskByCourse> {
    const params = new HttpParams()
      .set('idTask', idTask.toString())
      .set('idCourse', idCourse.toString());
    return this.http.get<IResponseTaskByCourse>(environment.remoteServiceBaseUrl+'User/getStudentPresentTask', { headers: this.headers, params: params});
  }

}
