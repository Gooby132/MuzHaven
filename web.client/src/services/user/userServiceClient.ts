import axios from "axios";
import { EMAIL_GROUP_CODE, LoginRequest, LoginResponse, RegisterRequest, RegisterResponse, CreateProjectRequest, CreateProjectResponse } from "./contracts";
import { ErrorDto } from "services/commons/contracts";

const USER_SERVICE_BASE = "http://localhost:8080/api/Users";
const REGISTER_USER_ENDPOINT = "/register-user";
const LOGIN_USER_ENDPOINT = "/login-user";
const CREATE_PROJECT_ENDPOINT = "/create-project";
export const CLIENT_INTERNAL_ERROR = 999;

export const validateRegisterData = ({
  email,
  password,
  stageName,
  firstName,
  lastName,
  bio,
}: RegisterRequest): [boolean, ErrorDto[]] => {
  let errors: ErrorDto[] = [];

  return [true, errors];
};

export const registerUser = async ({
  email,
  password,
  stageName,
  firstName,
  lastName,
  bio,
}: RegisterRequest): Promise<RegisterResponse> => {
  const validation = validateRegisterData({
    email,
    password,
    stageName,
    firstName,
    lastName,
    bio,
  });

  if (!validation[0])
    return {
      isError: true,
      errors: validation[1],
    };

  try {
    const res = await axios.post(
      `${USER_SERVICE_BASE}${REGISTER_USER_ENDPOINT}`,
      {
        password,
        email,
        stageName,
        firstName,
        lastName,
        bio,
      }
    );

    return {
      result: {
        token: res.data.token,
        user: {
          id: res.data.user.id,
          bio: res.data.user.bio,
          email: res.data.user.email,
          firstName: res.data.user.firstName,
          lastName: res.data.user.lastName,
          stageName: res.data.user.stageName,
        },
      },
      isError: false,
    };
  } catch (e: any) {
    switch (e.response.status) {
      case 500:
        return {
          isError: true,
          errors: [
            {
              code: 1,
              group: 500,
              message: "internal error",
            },
          ],
        };
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
    }

    return {
      isError: true,
    };
  }
};

export const validateLoginRequest = ({
  email,
  password,
}: LoginRequest): [boolean, ErrorDto[]] => {
  let errors: ErrorDto[] = [];

  return [true, errors];
};

export const loginUser = async ({
  email,
  password,
}: LoginRequest): Promise<LoginResponse> => {
  const validation = validateLoginRequest({
    email,
    password,
  });

  if (!validation[0])
    return {
      isError: true,
      errors: validation[1],
    };

  try {
    const res = await axios.post(`${USER_SERVICE_BASE}${LOGIN_USER_ENDPOINT}`, {
      password,
      email,
    });

    return {
      result: {
        token: res.data.token,
        user: {
          id: res.data.user.id,
          bio: res.data.user.bio,
          email: res.data.user.email,
          firstName: res.data.user.firstName,
          lastName: res.data.user.lastName,
          stageName: res.data.user.stageName,
        },
      },
      isError: false,
    };
  } catch (e: any) {
    if (e.code === "ERR_NETWORK")
      return {
        isError: true,
        errors: [
          {
            code: 1,
            group: 500,
            message: "internal error",
          },
        ],
      };

    switch (e.response.status) {
      case 500:
        return {
          isError: true,
          errors: [
            {
              code: 1,
              group: 500,
              message: "internal error",
            },
          ],
        };
      case 404:
        return {
          isError: true,
          errors: [
            {
              group: EMAIL_GROUP_CODE,
              code: 2,
              message: "Email was not found",
            },
          ],
        };
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
    }

    return {
      isError: true,
    };
  }
};

export const createProject = async ({project, token}: CreateProjectRequest): Promise<CreateProjectResponse> => {
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
      `${USER_SERVICE_BASE}${CREATE_PROJECT_ENDPOINT}`,
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
}
