import { CircleButton } from "components/atoms/buttons/CircleButton";
import plus from "assets/icons/plus.svg";
import styled from "styled-components";
import { UploadStemModal } from "../modals/UploadStemModal";
import { useEffect, useState } from "react";
import { getStems, uploadStem } from "services/stem/stemServiceClient";
import { GetStemsResponse } from "services/stem/contracts";
import { StemsLayout } from "components/layout/stem/StemsLayout";

const Container = styled.div``;

type Props = {
  creatorId: string;
  projectId: string;
};

export const StemsTab = ({ projectId, creatorId }: Props) => {
  const [showModal, setShowModal] = useState<boolean>(false);
  const [stems, setStems] = useState<GetStemsResponse>();

  useEffect(() => {
    const fetchedStems = async () => {
      const response = await getStems({
        projectId,
      });

      if(response.isError)
        return;
      
      setStems(response.result)
    };
    fetchedStems()
  }, [projectId]);

  return (
    <Container>
      <CircleButton
        onClick={() => setShowModal((prev) => !prev)}
        radius="2.5rem"
      >
        <img src={plus} style={{ width: "1.7rem", height: "1.7rem" }} />
      </CircleButton>
      <UploadStemModal
        closeModalClicked={() => setShowModal((prev) => !prev)}
        showModal={showModal}
        onSubmit={(stem, file) => {
          uploadStem({
            stem: stem,
            file: file,
          });
        }}
        creatorId={creatorId}
        projectId={projectId}
      />
      <StemsLayout stems={stems?.stems} />
    </Container>
  );
};
