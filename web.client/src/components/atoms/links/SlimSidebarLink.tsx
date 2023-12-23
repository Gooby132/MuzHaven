import React, { PropsWithChildren } from "react";
import { NavLink } from "react-router-dom";
import styled from "styled-components";

const Container = styled(NavLink)<{ isActive: boolean }>`
  background-color: ${({ theme, isActive }) =>
    !isActive ? theme.primary : theme.lightAccent};
  color: ${({ theme }) => theme.text};
  width: 80%;
  min-height: 3rem;
  text-align: center;
  line-height: 2.7rem;
  font-weight: bold;
  text-decoration:none;
`;

type Props = {
  text: string;
  to: string;
  isActive: boolean;
};

export const SlimSidebarLink = ({ to, text, isActive }: Props) => {
  return (
    <Container to={to} isActive={isActive}>
      {text}
    </Container>
  );
};
