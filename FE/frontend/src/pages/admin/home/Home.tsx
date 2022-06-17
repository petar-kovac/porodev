import axios from 'axios';
import { FC, useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import styled from 'styled-components';

import theme from 'theme/theme';
import GridCard from 'components/card/GridCard';
import PList from 'components/list/List';
import Spinner from 'components/spinner/Spinner';
import PButton from 'components/buttons/PButton';

import useAdminsData from '../admins/hooks/useAdminsData';

const Home: FC = () => {
  const [filesData, setFilesData] = useState<[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>();

  const { findData, data } = useAdminsData();

  useEffect(() => {
    const fetchCards = async () => {
      setIsLoading(true);
      try {
        const res = await axios.get(
          `${process.env.REACT_APP_MOCK_URL_2}/dashboard`,
        );
        setFilesData(res.data);
      } catch (err) {
        console.log(err);
      } finally {
        setIsLoading(false);
      }
    };
    fetchCards();
    findData();
  }, []);

  return (
    <StyledPage>
      <StyledUploadWrapper>
        <StyledListWrapper>
          <StyledHeading>Admins</StyledHeading>
          <PList data={data?.slice(0, 4)} />
        </StyledListWrapper>
        <StyledListWrapper>
          <StyledHeading>Users</StyledHeading>
          <PList data={data?.reverse().slice(0, 4)} />
        </StyledListWrapper>
      </StyledUploadWrapper>
      {!isLoading ? (
        <StyledCardListWrapper>
          <StyledCardHeading>
            Here is the preview of the latest uploaded files
          </StyledCardHeading>
          <StyledCardWrapper>
            {/* fix when backend is implemented */}
            {filesData?.slice(0, 5).map((value: any, index: any) => (
              <GridCard
                heading={value?.name}
                description={value?.description}
                image={value?.image}
              />
            ))}
          </StyledCardWrapper>
          <StyledShowMoreButton>
            <Link to="/files">
              <PButton
                text="Show more files"
                color="#fff"
                borderRadius="12px"
                background={theme.colors.primary}
              />
            </Link>
          </StyledShowMoreButton>
        </StyledCardListWrapper>
      ) : (
        <StyledSpinnerWrappper>
          <Spinner color="#000" size={42} speed={1.2} />
        </StyledSpinnerWrappper>
      )}
    </StyledPage>
  );
};

const StyledSpinnerWrappper = styled.div`
  display: flex;
  flex: 1.1;
  justify-content: center;
  align-items: center;
`;
const StyledHeading = styled.div`
  font-size: 42px;
  display: flex;
  font-weight: 600;
  margin: 20px 0;
`;
const StyledPage = styled.div`
  display: flex;
  flex-direction: column;
  height: 100%;
`;

const StyledListWrapper = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  flex: 1;
`;
const StyledUploadWrapper = styled.div`
  display: flex;
  flex: 1.1;
  justify-content: center;
  align-items: center;
  margin: 0 100px;
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
  justify-content: center;
  align-items: flex-start;
  overflow-x: auto;
  height: 100px;
`;
const StyledCardHeading = styled.div`
  font-size: 20px;
  display: flex;
  justify-content: center;
  font-weight: 300;
  color: #555;
`;
const StyledShowMoreButton = styled.div`
  display: flex;
  justify-content: center;
  font-weight: 300;
  color: #555;
  margin-bottom: 5%;
`;

export default Home;
