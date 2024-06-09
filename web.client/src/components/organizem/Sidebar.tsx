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
  max-height: 100vh;

  > .header {
    color: ${({ theme }) => theme.text};
  }

  > .content {
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    height: 100%;
    overflow-y: scroll;
    background-color: ${({ theme }) => theme.accent};

    > .footer {
      margin-top: 2rem;
      padding-bottom: 1rem;
      display: flex;
      align-items: center;
      flex-direction: column;
      gap: 1rem;
    }
  }

  ::-webkit-scrollbar {
    width: 6px;
  }

  ::-webkit-scrollbar-track {
    background: ${({ theme }) => theme.primary};
    border-radius: 10px;
  }

  ::-webkit-scrollbar-thumb {
    background: ${({ theme }) => theme.secondary};
    border-radius: 10px;
  }

  ::-webkit-scrollbar-thumb:hover {
    background: ${({ theme }) => theme.lightAccent};
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
