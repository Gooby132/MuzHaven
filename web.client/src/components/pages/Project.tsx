import React, { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { useLoaderData } from "react-router";
import styled from "styled-components";
import { Tab, Tabs, TabList, TabPanel } from "react-tabs";
import { PageBase } from "components/layout/pages/PageBase";
import { PageTitle } from "components/atoms/texts/PageTitle";
import { ChangesTab } from "components/organizem/project/ChangesTab";
import { DetailsTab } from "components/organizem/project/DetailsTab";
import { StemsTab } from "components/organizem/stem/StemsTab";
import { ContributersTab } from "components/organizem/project/ContributersTab";
import { CompleteProjectDto } from "services/user/contracts";
import { useSelector } from "react-redux";
import { RootState } from "redux/store";

const Container = styled.div`
  color: ${({ theme }) => theme.text};
  .react-tabs__tab {
    display: inline-block;
    position: relative;
    list-style: none;
    padding: 6px 12px;
    cursor: pointer;
  }
  .react-tabs__tab--selected {
    background: ${({ theme }) => theme.text};
    border-bottom: ${({ theme }) => theme.accent} 0.3rem solid;
    color: black;
  }
  .react-tabs__tab-panel {
    display: none;
  }

  .react-tabs__tab-panel--selected {
    display: block;
  }
`;

type Props = {};

export const Project = ({}: Props) => {
  let location = useLocation();
  const navigate = useNavigate();
  const [tabSelected, setTabSelected] = useState<number>(0);
  const user = useSelector((state: RootState) => state.user);
  const project = useLoaderData() as CompleteProjectDto;

  const navigateHash = (index: number) => {
    switch (index) {
      case 1:
        navigate("#Details");
        break;
      case 2:
        navigate("#Stems");
        break;
      case 3:
        navigate("#Contributers");
        break;
      case 0:
      default:
        navigate("#Changes");
        break;
    }
  };

  useEffect(() => {
    switch (location.hash) {
      case "#Details":
        setTabSelected(1);
        break;
      case "#Stems":
        setTabSelected(2);
        break;
      case "#Contributers":
        setTabSelected(4);
        break;
      case "#Changes":
      default:
        setTabSelected(0);
        break;
    }
  }, [location]);

  return (
    <PageBase>
      <Container>
        <PageTitle text={project.title} />
        <Tabs selectedIndex={tabSelected} onSelect={(x) =>navigateHash(x)}>
          <TabList>
            <Tab>Changes</Tab>
            <Tab>Details</Tab>
            <Tab>Stems</Tab>
            <Tab>Contributers</Tab>
          </TabList>

          <TabPanel>
            <ChangesTab />
          </TabPanel>
          <TabPanel>
            <DetailsTab project={project} />
          </TabPanel>
          <TabPanel>
            <StemsTab projectId={project.id} creatorId={user.id!} />
          </TabPanel>
          <TabPanel>
            <ContributersTab />
          </TabPanel>
        </Tabs>
      </Container>
    </PageBase>
  );
};
