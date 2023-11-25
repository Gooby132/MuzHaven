import React from "react";
import { PageTitle } from "../atoms/texts/PageTitle";
import styled from "styled-components";
import { RegisterForm } from "../layout/RegisterForm";
import { RegisterData, registerUser } from "../../services/user/userServiceClient";

const Container = styled.div``;

type Props = {};

export const Register = (props: Props) => {
  const onSubmit = (args: RegisterData) => {
    console.log(args)
    var yoyo = registerUser(args)
  } 

  return (
    <Container>
      <PageTitle text="Register" />
      <RegisterForm onSubmit={onSubmit} />
    </Container>
  );
};
