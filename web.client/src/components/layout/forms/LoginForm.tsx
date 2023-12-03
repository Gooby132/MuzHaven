import React, { useState } from "react";
import styled from "styled-components";
import {
  LoginData,
  validateLoginData,
} from "../../../services/user/userServiceClient";
import { SubmitInput } from "../../atoms/form/SubmitInput";
import { TextInput } from "../../atoms/form/TextInput";

const Container = styled.form`
  display: flex;
  flex-direction: column;
`;

const Row = styled.div`
  display: flex;
  flex-direction: row;
`;

type Props = {
  onSubmit: (args: LoginData) => void;
  formErrors?: LoginFormErrors;
};

export type LoginFormErrors = {
  emailError?: string;
  passwordError?: string;
}

export const LoginForm = ({formErrors, onSubmit}: Props) => {
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");

  return (
    <Container
      onSubmit={(e) => {
        e.preventDefault();

        onSubmit({
          email,
          password,
        });
      }}
    >
      <Row>
        <TextInput
          error={formErrors?.emailError}
          name="email"
          text="Email"
          onChange={(email) => setEmail(email)}
        />
        <TextInput
          error={formErrors?.passwordError}
          name="password"
          text="Password"
          onChange={(password) => setPassword(password)}
        />
      </Row>
      <Row>
        <SubmitInput text="Login" />
      </Row>
    </Container>
  );
};
