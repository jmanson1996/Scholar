export interface ICourseTaskView {
    result:              Result;
    targetUrl:           null;
    success:             boolean;
    error:               null;
    unAuthorizedRequest: boolean;
    __abp:               boolean;
}

export interface Result {
    title:        string;
    description:  string;
    deliveryDate: Date;
    courseId:     number;
    id:           number;
}
