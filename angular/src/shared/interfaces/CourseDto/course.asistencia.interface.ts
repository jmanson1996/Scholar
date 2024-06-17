export interface AssistanceFullViewDto {
    date:          Date;
    userStudentId: number;
    courseId:      number;
    isPresent:     boolean;
    fullName : string | null;
}

export interface AssistanceFullDto {
    date:          Date;
    userStudentId: number;
    courseId:      number;
    isPresent:     boolean;
}
