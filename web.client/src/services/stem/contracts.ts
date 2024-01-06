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

export interface GetStreamResponse {
  playback: ArrayBuffer
}

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
  stageName: string,
  time?: number
}

export interface CreateCommentResponse {}
//dtos

export interface CommenterDto {
  id: string,
  firstName: string,
  lastName: string,
  stageName: string
}

export interface CommentDto {
  commenter: CommenterDto,
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
  musicFile?: MusicFile;
  comments: CommentDto[];
  file?: FileList;
}

export interface MusicFile {
  format: string,
}