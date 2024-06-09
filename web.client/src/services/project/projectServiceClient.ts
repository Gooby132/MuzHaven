import axios from "axios";
import {
  FetchProjectByIdResponse,
  FetchProjectByIdRequest,
  CreateProjectRequest,
  CreateProjectResponse,
  FetchProjectsResponse,
  DeleteProjectRequest,
  DeleteProjectResponse,
} from "./contracts";
import { ErrorDto } from "services/commons/contracts";

const PROJECT_SERVICE_BASE = "http://localhost:8080/api/Projects";
const FETCH_PROJECTS = "/get-projects";
const CREATE_PROJECT = "/create-project";
const DELETE_PROJECT = "/delete-project";
const FETCH_BY_ID = "/get-by-id";

export const CLIENT_INTERNAL_ERROR = 999;

export type FetchClientProjectsRequest = {
  token: string;
};

export const fetchProjectById = async ({
  id,
  token,
}: FetchProjectByIdRequest): Promise<FetchProjectByIdResponse> => {
  try {
    const res = await axios.get(`${PROJECT_SERVICE_BASE}${FETCH_BY_ID}`, {
      params: {
        id,
      },
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });

    return {
      isError: false,
      result: res.data.project,
    };
  } catch (e: any) {
    switch (e.response?.status) {
      case 400:
        return {
          isError: true,
          errors: e.response.data.map(
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
        console.log(e);
        return {
          isError: true,
        };
    }
  }
};

export const fetchProjects = async ({
  token,
}: FetchClientProjectsRequest): Promise<FetchProjectsResponse> => {
  try {
    const res = await axios.get(`${PROJECT_SERVICE_BASE}${FETCH_PROJECTS}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });

    return {
      isError: false,
      result: {
        projects: res.data.projects,
      },
    };
  } catch (e: any) {
    switch (e.response?.status) {
      case 400:
        return {
          isError: true,
          errors: e.response.data.map(
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
        console.log(e.response);
        return {
          isError: true,
        };
    }
  }
};

export const createProject = async ({
  token,
  project,
}: CreateProjectRequest): Promise<CreateProjectResponse> => {
  try {
    if (token === undefined) {
      return {
        isError: true,
        errors: [
          {
            group: CLIENT_INTERNAL_ERROR,
            code: 0,
            message: "token was not provided",
          },
        ],
      };
    }

    const res = await axios.post(
      `${PROJECT_SERVICE_BASE}${CREATE_PROJECT}`,
      {
        project,
        token,
      },
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );
    return {
      isError: false,
      project: {
        id: res.data.id,
        title: res.data.title,
        album: res.data.album,
        createdInUtc: res.data.createdInUtc,
        beatsPerMinute: res.data.beatsPerMinute,
        description: res.data.description,
        releaseInUtc: res.data.releaseInUtc,
        musicalProfile:
          res.data.musicalProfile !== undefined
            ? {
                key: res.data.musicalProfile.key,
                scale: res.data.musicalProfile.scale,
              }
            : undefined,
      },
    };
  } catch (e: any) {
    switch (e.response?.status || 0) {
      case 400:
        return {
          isError: true,
          errors: e.response.data.map((error: ErrorDto) => {
            return {
              code: error.code,
              group: error.group,
              message: error.message,
            };
          }),
        };
      default:
        console.log(e);
        return {
          isError: true,
        };
    }
  }
};

export const deleteProject = async ({
  id,
  token
}: DeleteProjectRequest): Promise<DeleteProjectResponse> => {
  try {
    if (token === undefined) {
      return {
        isError: true,
        errors: [
          {
            group: CLIENT_INTERNAL_ERROR,
            code: 0,
            message: "token was not provided",
          },
        ],
      };
    }

    await axios.delete(
      `${PROJECT_SERVICE_BASE}${DELETE_PROJECT}`,
      {
        params: {
          id: id
        },
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );

    return {
      isError: false,
    }
  } catch (e: any) {
    switch (e.response?.status || 0) {
      case 400:
        return {
          isError: true,
          errors: e.response.data.map((error: ErrorDto) => {
            return {
              code: error.code,
              group: error.group,
              message: error.message,
            };
          }),
        };
      default:
        console.log(e);
        return {
          isError: true,
        };
    }
  }
};
