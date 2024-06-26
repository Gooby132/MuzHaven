import axios from "axios";
import {
  CreateCommentRequest,
  CreateCommentResponse,
  GetStemRequest,
  GetStemResponse,
  GetStemsRequest,
  GetStemsResponse,
  GetStreamRequest,
  GetStreamResponse,
  UploadStemRequest,
  UploadStemResponse,
} from "./contracts";
import { ResponseDto } from "services/commons/contracts";

export const STEM_SERVICE_BASE = "http://localhost:8080/api/Stem";
const GET_STEMS = "/get-stems";
const UPLOAD_STEMS = "/upload-stem";
export const GET_STREAM = "/get-playback";
const GET_STEM = "/get-stem";
const CREATE_COMMENT = "/create-comment";

export const getStems = async (
  request: GetStemsRequest
): Promise<ResponseDto<GetStemsResponse>> => {
  try {
    const response = await axios.get(`${STEM_SERVICE_BASE}${GET_STEMS}`, {
      params: {
        projectId: request.projectId,
      },
    });

    return {
      isError: false,
      result: response.data,
    };
  } catch (error: any) {
    switch (error.response?.status) {
      case 400:
        return {
          isError: true,
          errors: error.response.data.map(
            (error: { code: number; group: number; message?: string }) => {
              return {
                code: error.code,
                group: error.group,
                message: error.message,
              };
            }
          ),
        };
      default:
        console.log(error);
        return {
          isError: true,
        };
    }
  }
};

export const uploadStem = async (
  request: UploadStemRequest
): Promise<ResponseDto<UploadStemResponse>> => {
  try {
    const form = new FormData();
    form.append("projectId", request.stem.projectId);
    form.append("creatorId", request.stem.creatorId);
    form.append("name", request.stem.name);
    form.append("description", request.stem.description);
    form.append("instrument", request.stem.instrument);
    form.append("file", request.file[0]);

    const response = await axios.post(
      `${STEM_SERVICE_BASE}${UPLOAD_STEMS}`,
      form
    );

    return {
      isError: false,
      result: response.data,
    };
  } catch (error: any) {
    switch (error.response?.status) {
      case 400:
        return {
          isError: true,
          errors: error.response.data.map(
            (error: { code: number; group: number; message?: string }) => {
              return {
                code: error.code,
                group: error.group,
                message: error.message,
              };
            }
          ),
        };
      default:
        console.log(error);
        return {
          isError: true,
        };
    }
  }
};

export const getStream = async (
  request: GetStreamRequest
): Promise<ResponseDto<GetStreamResponse>> => {
  try {
    const response = await axios.get(`${STEM_SERVICE_BASE}${GET_STREAM}`,{
      params: {
        stemId: request.stemId
      },
      responseType: 'arraybuffer'
    });

    return {
      isError: false,
      result: response.data,
    };
  } catch (error: any) {
    switch (error.response?.status) {
      case 400:
        return {
          isError: true,
          errors: error.response.data.map(
            (error: { code: number; group: number; message?: string }) => {
              return {
                code: error.code,
                group: error.group,
                message: error.message,
              };
            }
          ),
        };
      default:
        console.log(error);
        return {
          isError: true,
        };
    }
  }
};

export const getStem = async (
  request: GetStemRequest
): Promise<ResponseDto<GetStemResponse>> => {
  try {
    const response = await axios.get(`${STEM_SERVICE_BASE}${GET_STEM}`);

    return {
      isError: false,
      result: response.data,
    };
  } catch (error: any) {
    switch (error.response?.status) {
      case 400:
        return {
          isError: true,
          errors: error.response.data.map(
            (error: { code: number; group: number; message?: string }) => {
              return {
                code: error.code,
                group: error.group,
                message: error.message,
              };
            }
          ),
        };
      default:
        console.log(error);
        return {
          isError: true,
        };
    }
  }
};

export const createComment = async (
  request: CreateCommentRequest
): Promise<ResponseDto<CreateCommentResponse>> => {
  try {
    const response = await axios.post(`${STEM_SERVICE_BASE}${CREATE_COMMENT}`,
    request)

    return {
      isError: false,
      result: response
    }
  } catch (error: any) {
    switch (error.response?.status) {
      case 400:
        return {
          isError: true,
          errors: error.response.data.map(
            (error: { code: number; group: number; message?: string }) => {
              return {
                code: error.code,
                group: error.group,
                message: error.message,
              };
            }
          ),
        };
      default:
        console.log(error);
        return {
          isError: true,
        };
    }
  }
};
