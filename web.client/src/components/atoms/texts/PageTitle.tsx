import React from "react";
import styled from "styled-components";

const Container = styled.div`
  font-size: 1.5em;
  margin-bottom: 1em;
`;

type Props = {
  text: string;
};

export const PageTitle = ({ text }: Props) => {
  return <Container>{text}</Container>;
};
