import { FC, useEffect } from 'react';
import styled from 'styled-components';
import PCard from '../../components/card/PCard';
import PUpload from '../../components/upload/PUpload';
import { useAuthStateValue } from '../../context/AuthContext';

const Home: FC = () => {
  const { isAuthenticated, testMessage } = useAuthStateValue();

  return (
    <StyledPage>
      <StyledUploadWrapper>
        <StyledHeading>File upload:</StyledHeading>
        <PUpload />
      </StyledUploadWrapper>
      <StyledCardListWrapper>
        <StyledCardHeading>
          Here is the preview of the latest uploaded files
        </StyledCardHeading>
        <StyledCardWrapper>
          <PCard />
          <PCard />
          <PCard />
          <PCard />
          <PCard />
        </StyledCardWrapper>
      </StyledCardListWrapper>
    </StyledPage>
  );
};

const StyledHeading = styled.div`
  font-size: 42px;
  display: flex;
  justify-content: center;
  font-weight: 600;
  margin: 20px 0;
`;
const StyledPage = styled.div`
  display: flex;
  flex-direction: column;
  height: 100%;
`;
const StyledUploadWrapper = styled.div`
  flex: 1.1;
`;
const StyledCardListWrapper = styled.div`
  display: flex;
  flex-direction: column;
  flex: 1;
  gap: 20px;
`;
const StyledCardWrapper = styled.div`
  flex: 1;
  display: flex;
  gap: 20px;
  /* margin: 0 5% 10px; */
  justify-content: center;
  align-items: flex-start;
  overflow-x: auto;
`;
const StyledCardHeading = styled.div`
  font-size: 20px;
  display: flex;
  justify-content: center;
  font-weight: 300;
  color: #555;
`;

export default Home;
