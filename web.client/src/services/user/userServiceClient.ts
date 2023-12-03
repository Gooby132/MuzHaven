import axios from "axios";

const USER_SERVICE_BASE = "http://localhost:8080/api/Users";
const REGISTER_USER_ENDPOINT = "/register-user";
const LOGIN_USER_ENDPOINT = "/login-user";

export const PASSWORD_GROUP_CODE = 1
export const EMAIL_GROUP_CODE = 2
export const FIRST_NAME_GROUP_CODE = 3
export const LAST_NAME_GROUP_CODE = 4
export const BIO_GROUP_CODE = 5
export const STAGE_NAME_GROUP_CODE = 6

export type RegisterData = {
  email: string;
  password: string;
  stageName: string;
  firstName: string;
  lastName: string;
  bio: string;
};

export type LoginData = {
  email: string,
  password: string,
}

export type User = {
  id: string,
  bio: string,
  email: string,
  firstName: string,
  lastName: string,
  stageName: string,
}

export type Error = {
  code: number;
  group: number;
  message?: string;
};

export type RegisterResponse = {
  result?: LoginResult;
  errors?: Error[];
  isError: boolean;
};

export type LoginResponse = {
  result?: LoginResult;
  errors?: Error[];
  isError: boolean;
};

export type LoginResult = {
  user: User
  token: string
}

export const validateRegisterData = ({
  email,
  password,
  stageName,
  firstName,
  lastName,
  bio,
}: RegisterData): [boolean, Error[]] => {
  let errors: Error[] = [];

  return [true, errors];
};

export const registerUser = async ({
  email,
  password,
  stageName,
  firstName,
  lastName,
  bio,
}: RegisterData): Promise<RegisterResponse> => {
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
        }
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
          errors: 
            e.response.data.map(
              (error: { code: number; group: number; message?: string }) => {
              return {
                code: error.code, 
                group: error.group,
                message: error.message,
              }}
            ),
        }
    }

    return {
      isError:true
    };
  }
};

export const validateLoginData = ({
  email,
  password,
}: LoginData): [boolean, Error[]] => {
  let errors: Error[] = [];

  return [true, errors];
};

export const loginUser = async ({
  email,
  password,
}: LoginData): Promise<LoginResponse> => {
  const validation = validateLoginData({
    email,
    password,
  });

  if (!validation[0])
    return {
      isError: true,
      errors: validation[1],
    };

  try {
    const res = await axios.post(
      `${USER_SERVICE_BASE}${LOGIN_USER_ENDPOINT}`,
      {
        password,
        email,
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
        }
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
      case 404:
        return {
          isError: true,
          errors: [{
            group: EMAIL_GROUP_CODE,
            code: 2,
            message: "Email was not found"
          }]
        }
      case 400:
        return {
          isError: true,
          errors: 
            e.response.data.map(
              (error: { code: number; group: number; message?: string }) => {
              return {
                code: error.code, 
                group: error.group,
                message: error.message,
              }}
            ),
        }
    }

    return {
      isError:true
    };
  }
};