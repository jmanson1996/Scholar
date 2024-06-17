import { ICourse, IResponseCourse } from '../interfaces/CourseDto/course.interface';
import { HttpClient, HttpHeaders, HttpParams  } from '@angular/common/http';
import { Injectable, computed, inject, signal } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ICourseByID } from '../interfaces/CourseDto/course.getId.interface';
import {AssistanceListFullDto} from '../interfaces/CourseDto/course.assistance.full.interface';
import { ICourseTaskFull } from '@shared/interfaces/CourseDto/course.task.full.interface';
import { Item as ItemTask} from '@shared/interfaces/UserDto/courseStudentTaskByCourse.view';
import { ICourseTaskView } from '@shared/interfaces/CourseDto/course.task.view';

@Injectable({
  providedIn: 'root'
})
export class ServicesCourseService {
  private http = inject(HttpClient);

  private headers = new HttpHeaders({
    'Authorization': 'Bearer '+ localStorage.getItem('accessToken'),
  })

  createCourse(course: ICourse): Observable<ICourse> {
    return this.http.post<ICourse>(environment.remoteServiceBaseUrl+'Course/Create', course, { headers: this.headers });
  }

  getAllByUser(idUser: number, skipCount: number, maxResultCount: number): Observable<IResponseCourse> {
    const params = new HttpParams()
      .set('idUser', idUser.toString())
      .set('SkipCount', skipCount.toString())
      .set('MaxResultCount', maxResultCount.toString());

    return this.http.get<IResponseCourse>(environment.remoteServiceBaseUrl+'Course/GetAllByUser', { params: params, headers: this.headers});
  }

  getCourseById(id: string): Observable<ICourseByID> {
    const params = new HttpParams()
      .set('Id', id.toString());
    return this.http.get<ICourseByID>(environment.remoteServiceBaseUrl+'Course/Get', { headers: this.headers, params: params});
  }

  editCourse(course: ICourse): Observable<ICourse> {
    return this.http.put<ICourse>(environment.remoteServiceBaseUrl+'Course/Update', course ,{ headers: this.headers});
  }

  deleteCourse(id : string) : Observable<ICourse> {
    const params = new HttpParams()
      .set('Id', id.toString());
    return this.http.delete<ICourse>(environment.remoteServiceBaseUrl+'Course/Delete', { headers: this.headers, params: params});
  }

  createCourseAssistance(course: AssistanceListFullDto[]): Observable<AssistanceListFullDto[]> {
    return this.http.post<AssistanceListFullDto[]>(environment.remoteServiceBaseUrl+'Assistance/createAssistance', course, { headers: this.headers });
  }

  editCourseAssistance(course: AssistanceListFullDto[]): Observable<AssistanceListFullDto[]> {
    return this.http.put<AssistanceListFullDto[]>(environment.remoteServiceBaseUrl+'Assistance/updateAssistance', course, { headers: this.headers });
  }

  createCourseTaskAssistance(task: ICourseTaskFull): Observable<ICourseTaskFull> {
    return this.http.post<ICourseTaskFull>(environment.remoteServiceBaseUrl+'Asignation/Create', task, { headers: this.headers });
  }

  createOrUpdateAssistance(course: ItemTask[]): Observable<ItemTask[]> {
    return this.http.post<ItemTask[]>(environment.remoteServiceBaseUrl+'DeliveryAssignment/createOrUpdateAssistance', course, { headers: this.headers });
  }

  deleteStudentPresentTask(id : string) : Observable<ICourse> {
    const params = new HttpParams()
      .set('idTask', id.toString());
    return this.http.delete<ICourse>(environment.remoteServiceBaseUrl+'User/deleteStudentPresentTask', { headers: this.headers, params: params});
  }

  getAsignationById(id: string): Observable<ICourseTaskView> {
    const params = new HttpParams()
      .set('Id', id.toString());
    return this.http.get<ICourseTaskView>(environment.remoteServiceBaseUrl+'Asignation/Get', { headers: this.headers, params: params});
  }

  editCourseTaskAssistance(task: ICourseTaskFull): Observable<ICourseTaskFull> {
    return this.http.put<ICourseTaskFull>(environment.remoteServiceBaseUrl+'Asignation/Update', task, { headers: this.headers });
  }
}
