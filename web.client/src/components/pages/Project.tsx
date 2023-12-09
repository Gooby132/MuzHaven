import React from "react";
import { useLoaderData } from "react-router";
import styled from "styled-components";
import { Tab, Tabs, TabList, TabPanel } from "react-tabs";
import { PageBase } from "components/layout/pages/PageBase";
import { PageTitle } from "components/atoms/texts/PageTitle";
import { ChangesTab } from "components/organizem/project/ChangesTab";
import { DetailsTab } from "components/organizem/project/DetailsTab";
import { StemsTab } from "components/organizem/project/StemsTab";
import { ContributersTab } from "components/organizem/project/ContributersTab";

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
    background: ${({theme}) => theme.text};
    border-bottom: ${({theme}) => theme.accent} .3rem solid;
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
  const project = useLoaderData() as any;
  console.log(project);

  return (
    <PageBase>
      <Container>
        <PageTitle text={project.title} />
        <Tabs>
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
            <DetailsTab />
          </TabPanel>
          <TabPanel>
            <StemsTab />
          </TabPanel>
          <TabPanel>
            <ContributersTab />
          </TabPanel>
        </Tabs>
      </Container>
    </PageBase>
  );
};
