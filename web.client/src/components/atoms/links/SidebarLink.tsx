import React, { PropsWithChildren } from "react";
import { NavLink, useMatches } from "react-router-dom";
import { CompleteProjectDto } from "services/user/contracts";
import styled from "styled-components";

const Container = styled(NavLink)<{ isActive: boolean }>`
  background-color: ${({ theme, isActive }) =>
    !isActive ? theme.primary : theme.lightAccent};
  width: 80%;
  min-height: 5.5em;
  text-decoration: none;
  color: ${({ theme }) => theme.text};

  padding: .4em;

  &.active {
    background-color: ${({ theme }) => theme.lightAccent};
  }
`;

type Props = {
  project: CompleteProjectDto;
  to: string;
  isActive: boolean;
};

export const SidebarLink = ({ project, to, isActive }: Props) => {
  return (
    <Container to={to} isActive={isActive}>
      <h2>{project.title}</h2>
    </Container>
  );
};
