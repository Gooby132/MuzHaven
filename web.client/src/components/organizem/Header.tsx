import { ReactNode } from "react";
import styled from "styled-components";

interface Props {
  layout: ReactNode;
}

const Container = styled.header``;

export const Header = ({ layout }: Props) => {
  return <Container>{layout}</Container>;
};
