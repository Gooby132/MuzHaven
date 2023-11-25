import React, { useState } from "react";
import styled from "styled-components";
import {
  RegisterData,
  validateRegisterData,
} from "../../services/user/userServiceClient";
import { TextInput } from "../atoms/form/TextInput";
import { SubmitInput } from "../atoms/form/SubmitInput";

const Container = styled.form`
  display: flex;
  flex-direction: column;
`;

const Row = styled.div`
  display:flex;
  flex-direction: row;
`;

type Props = {
  onSubmit: (args: RegisterData) => void;
};

export const RegisterForm = ({ onSubmit }: Props) => {
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
          name="email"
          text="Email"
          onChange={(email) => setEmail(email)}
        />
        <TextInput
          name="password"
          text="Password"
          onChange={(password) => setPassword(password)}
        />
      </Row>
      <Row>
        <TextInput
          name="stage-name"
          text="Stage Name"
          onChange={(stageName) => setStageName(stageName)}
        />
      </Row>
      <Row>
        <TextInput
          name="first-name"
          text="First Name"
          onChange={(firstName) => setFirstName(firstName)}
        />
        <TextInput
          name="last -name"
          text="Last Name"
          onChange={(lastName) => setLastName(lastName)}
        />
      </Row>
      <Row>
        <TextInput name="bio" text="Bio" onChange={(bio) => setBio(bio)} />
      </Row>
      <Row>
        <SubmitInput text="Register" />
      </Row>
    </Container>
  );
};
