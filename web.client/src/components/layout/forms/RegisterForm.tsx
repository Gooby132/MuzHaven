import React, { useState } from "react";
import styled from "styled-components";
import {
  validateRegisterData,
} from "../../../services/user/userServiceClient";
import { TextInput } from "../../atoms/form/TextInput";
import { SubmitInput } from "../../atoms/form/SubmitInput";
import { RegisterRequest } from "services/user/contracts";

const Container = styled.form`
  display: flex;
  flex-direction: column;
`;

const Row = styled.div`
  display: flex;
  flex-direction: row;
`;

type Props = {
  onSubmit: (args: RegisterRequest) => void;
  emailError?: string;
  stageNameError?: string;
  firstNameError?: string;
  lastNameError?: string;
  bioError?: string;
  passwordError?: string;
};

export const RegisterForm = ({
  onSubmit,
  emailError,
  stageNameError,
  firstNameError,
  lastNameError,
  bioError,
  passwordError,
}: Props) => {
  const [email, setEmail] = useState("");
  const [stageName, setStageName] = useState("");
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [bio, setBio] = useState("");
  const [password, setPassword] = useState("");

  return (
    <Container
      onSubmit={(e) => {
        e.preventDefault();

        onSubmit({
          email,
          password,
          stageName,
          firstName,
          lastName,
          bio,
        });
      }}
    >
      <Row>
        <TextInput
          error={emailError}
          name="email"
          text="Email"
          onChange={(email) => setEmail(email)}
        />
        <TextInput
          error={passwordError}
          name="password"
          text="Password"
          onChange={(password) => setPassword(password)}
        />
      </Row>
      <Row>
        <TextInput
          error={stageNameError}
          name="stage-name"
          text="Stage Name"
          onChange={(stageName) => setStageName(stageName)}
        />
      </Row>
      <Row>
        <TextInput
          error={firstNameError}
          name="first-name"
          text="First Name"
          onChange={(firstName) => setFirstName(firstName)}
        />
        <TextInput
          error={lastNameError}
          name="last-name"
          text="Last Name"
          onChange={(lastName) => setLastName(lastName)}
        />
      </Row>
      <Row>
        <TextInput
          error={bioError}
          name="bio"
          text="Bio"
          onChange={(bio) => setBio(bio)}
        />
      </Row>
      <Row>
        <SubmitInput text="Register" />
      </Row>
    </Container>
  );
};
