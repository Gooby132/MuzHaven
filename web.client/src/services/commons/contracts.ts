export interface ResponseDto<T> {
  isError: boolean
  errors?: ErrorDto[]
  result?: T
}

export type ErrorDto = {
  code: number;
  group: number;
  message?: string;
};