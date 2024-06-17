export interface IUserByIDResult {
    result:              IResult;
    targetUrl:           null;
    success:             boolean;
    error:               null;
    unAuthorizedRequest: boolean;
    __abp:               boolean;
}

export interface IResult {
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
