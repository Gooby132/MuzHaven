import axios, { AxiosError } from "axios";
import { config } from "process";

const USER_SERVICE_BASE = "http://localhost:8080/api/Users";
const REGISTER_USER_ENDPOINT = "/register-user";

export type RegisterData = {
  email: string;
  password: string;
  stageName: string;
  firstName: string;
  lastName: string;
  bio: string;
};

type Error = {
  code: number;
  text?: string;
};

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
}: RegisterData): Promise<[any, Error[] | null]> => {
  const validation = validateRegisterData({
    email,
    password,
    stageName,
    firstName,
    lastName,
    bio,
  });

  if (!validation[0]) return [null, validation[1]];

  try {
    console.log();
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

    return [res, null];
  } catch (e: any) {

    switch(e.response){
      case 500:
      return [null, [{
        text: "internal error",
        code:1
      }]];
      
    }

    return [null, null];
  }
};
