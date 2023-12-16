import React, { useState } from "react";
import styled from "styled-components";
import { PageTitle } from "../atoms/texts/PageTitle";
import { LoginForm, LoginFormErrors } from "../layout/forms/LoginForm";
import {
  EMAIL_GROUP_CODE,
  LoginRequest,
  PASSWORD_GROUP_CODE,
} from "../../services/user/contracts";
import { userActions } from "../../redux/features/user/userSlice";
import { useDispatch } from "react-redux";
import { loginUser } from "services/user/userServiceClient";
import { PageBase } from "components/layout/pages/PageBase";
import { Link } from "react-router-dom";

const Container = styled.div`
  color: ${({ theme }) => theme.text};
`;

type Props = {};

export const Login = (props: Props) => {
  const dispatch = useDispatch();
  const [formErrors, setFormErrors] = useState<LoginFormErrors>({
    emailError: undefined,
    passwordError: undefined,
  });

  const onSubmit = async (args: LoginRequest) => {
    const errors: LoginFormErrors = {
      emailError: undefined,
      passwordError: undefined,
    };

    const res = await loginUser(args);

    if (!res.isError) {
      dispatch(userActions.login(res));
    }

    if (res.isError) {
      res.errors?.forEach((error) => {
        switch (error.group) {
          case PASSWORD_GROUP_CODE:
            errors.passwordError = error.message;
            return;
          case EMAIL_GROUP_CODE:
            errors.emailError = error.message;
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
        <PageTitle text="Login" />
        <LoginForm formErrors={formErrors} onSubmit={onSubmit} />
        <Link to="/register" >register</Link>
      </Container>
    </PageBase>
  );
};
