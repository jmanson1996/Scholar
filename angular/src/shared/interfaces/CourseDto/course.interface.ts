export interface ICourse {
  name:          string;
  description:   string;
  duration:      number;
  maxStudents:   number;
  minStudents:   number;
  userTeacherId: number;
  id : number | null;
}


export interface IResponseCourse {
  result:              Result;
  targetUrl:           null;
  success:             boolean;
  error:               null;
  unAuthorizedRequest: boolean;
  __abp:               boolean;
}

export interface Result {
  totalCount: number;
  items:      Item[];
}

export interface Item {
  name:          string;
  description:   string;
  duration:      number;
  maxStudents:   number;
  minStudents:   number;
  userTeacherId: number;
  id:            number;
}

