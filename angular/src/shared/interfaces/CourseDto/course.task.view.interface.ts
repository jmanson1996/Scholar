// Generated by https://quicktype.io

export interface ITaskView {
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
    title:        string;
    description:  string;
    deliveryDate: Date;
    courseId:     number;
    id:           number;
}
