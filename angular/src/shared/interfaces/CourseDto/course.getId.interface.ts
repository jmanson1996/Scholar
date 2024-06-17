export interface ICourseByID {
    result:              IResult;
    targetUrl:           null;
    success:             boolean;
    error:               null;
    unAuthorizedRequest: boolean;
    __abp:               boolean;
  }

  export interface IResult {
    name:          string;
    description:   string;
    duration:      number;
    maxStudents:   number;
    minStudents:   number;
    userTeacherId: number;
    id:            number;
  }
