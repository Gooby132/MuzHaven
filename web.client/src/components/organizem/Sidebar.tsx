import { ReactNode } from "react";
import styled, { useTheme } from "styled-components";

type Props = {
  header: ReactNode;
  links: ReactNode[];
  footer: ReactNode[];
};

const Container = styled.nav`
  display: flex;
  flex-direction: column;

  > .header{
    color: ${({ theme }) => theme.text};
  }

  > .content {
    height: 100%;
    background-color: ${({ theme }) => theme.accent};
  }
`;

export const Sidebar = ({ header, links, footer }: Props) => {
  const theme = useTheme();

  return (
    <Container theme={theme}>
      <div className="header">{header}</div>
      <div className="content">
        <div className="links">{links}</div>
        <div className="footer">{footer}</div>
      </div>
    </Container>
  );
};
