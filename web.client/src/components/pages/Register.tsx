import React, { useState } from "react";
import styled from "styled-components";
import { RegisterForm } from "../layout/forms/RegisterForm";
import {
  RegisterRequest,
  RegisterResponse,
  PASSWORD_GROUP_CODE,
  EMAIL_GROUP_CODE,
  FIRST_NAME_GROUP_CODE,
  LAST_NAME_GROUP_CODE,
  BIO_GROUP_CODE,
  STAGE_NAME_GROUP_CODE,
} from "../../services/user/contracts";
import { userActions } from "../../redux/features/user/userSlice";
import { useDispatch } from "react-redux";
import { registerUser } from "services/user/userServiceClient";
import { PageBase } from "components/layout/pages/PageBase";

const Container = styled.div``;

type FormErrors = {
  email: string | undefined;
  password: string | undefined;
  stageError: string | undefined;
  firstName: string | undefined;
  lastName: string | undefined;
  bio: string | undefined;
};

type Props = {};

export const Register = (props: Props) => {
  const dispatch = useDispatch();

  const [formErrors, setFormErrors] = useState<FormErrors | null>(null);

  const onSubmit = async (args: RegisterRequest) => {
    const errors: FormErrors = {
      email: undefined,
      password: undefined,
      stageError: undefined,
      firstName: undefined,
      lastName: undefined,
      bio: undefined,
    };

    const res = await registerUser(args);

    if (!res.isError) {
      dispatch(userActions.login(res));
    }

    if (res.isError) {
      res.errors?.forEach((error) => {
        switch (error.group) {
          case PASSWORD_GROUP_CODE:
            errors.password = error.message;
            return;
          case EMAIL_GROUP_CODE:
            errors.email = error.message;
            return;
          case FIRST_NAME_GROUP_CODE:
            errors.firstName = error.message;
            return;
          case LAST_NAME_GROUP_CODE:
            errors.lastName = error.message;
            return;
          case BIO_GROUP_CODE:
            errors.bio = error.message;
            return;
          case STAGE_NAME_GROUP_CODE:
            errors.stageError = error.message;
            return;
          default:
            console.log(error);
        }
      });
    }

    setFormErrors(errors);
  };

  return (
    <PageBase>
      <Container>
        <h1>Register</h1>
        <RegisterForm
          passwordError={formErrors?.password}
          emailError={formErrors?.email}
          firstNameError={formErrors?.firstName}
          lastNameError={formErrors?.lastName}
          stageNameError={formErrors?.stageError}
          bioError={formErrors?.bio}
          onSubmit={onSubmit}
        />
      </Container>
    </PageBase>
  );
};
