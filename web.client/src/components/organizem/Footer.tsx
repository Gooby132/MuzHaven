import React from "react";
import styled from "styled-components";

const Container = styled.footer`
  display: flex;
  justify-content: center;
  min-height: 10vh;
  color: ${({theme}) => theme.text};
`;

export const Footer = () => {
  return <Container>MuzHaven</Container>;
};
