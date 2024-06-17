export interface IResponseCourseNotFilterFullView {
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
    fullName:      string;
    date:          null;
    userStudentId: number;
    courseId:      number;
    isPresent:     boolean;
    id:            number;
}
