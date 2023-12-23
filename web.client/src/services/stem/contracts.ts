export interface GetStemsRequest {
  projectId: string;
}

export interface GetStemsResponse {
  stems: CompleteStemDto[];
}

export interface UploadStemRequest {
  stem: StemDto;
  file: FileList;
}

export interface UploadStemResponse {
  stem: CompleteStemDto;
}

export interface GetStreamRequest {
  stemId: string;
}

export interface GetStreamResponse {}

export interface GetStemRequest {
  stemId: string;
}

export interface GetStemResponse {
  stem: CompleteStemDto;
}

export interface CreateCommentRequest {
  commenterId: string,
  stemId: string,
  text: string,
  time?: number
}

export interface CreateCommentResponse {}

//dtos

export interface CommentDto {
  commenterId: string,
  createdOnUtc: string,
  text: string,
  time?: number
}

export interface CompleteStemDto extends StemDto {
  id: string;
}

export interface StemDto {
  projectId: string;
  creatorId: string;
  name: string;
  instrument: string;
  description: string;
  comments: CommentDto[];
  file?: FileList;
}
