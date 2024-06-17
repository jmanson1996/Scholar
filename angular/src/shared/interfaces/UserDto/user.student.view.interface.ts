// Generated by https://quicktype.io

export interface IStudentUserResponse {
    result:              Result[];
    targetUrl:           null;
    success:             boolean;
    error:               null;
    unAuthorizedRequest: boolean;
    __abp:               boolean;
}

export interface Result {
    userName:      string;
    name:          string;
    surname:       string;
    emailAddress:  string;
    isActive:      boolean;
    fullName:      string;
    lastLoginTime: null;
    creationTime:  Date;
    roleNames:     string[];
    id:            number;
}
