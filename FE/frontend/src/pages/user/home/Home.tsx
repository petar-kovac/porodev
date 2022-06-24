import axios from 'axios';
import { FC, useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import styled from 'styled-components';

import GridCard from 'components/card/GridCard';
import Spinner from 'components/spinner/Spinner';
import PUpload from 'components/upload/PUpload';

import PButton from 'components/buttons/PButton';
import theme from 'theme/theme';
import {
  StyledCardHeading,
  StyledCardListWrapper,
  StyledCardWrapper,
  StyledHeading,
  StyledPage,
  StyledShowMoreButton,
  StyledSpinnerWrappper,
  StyledUploadWrapper,
} from './home-styled';

const Home: FC = () => {
  const [data, setData] = useState<[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>();

  useEffect(() => {
    const fetchCards = async () => {
      setIsLoading(true);
      try {
        await axios
          .get(`${process.env.REACT_APP_MOCK_URL}/files`)
          .then((res) => setData(res.data));
      } catch (err) {
        console.log(err);
      } finally {
        setIsLoading(false);
      }
    };
    fetchCards();
  }, []);

  return (
    <StyledPage>
      <StyledUploadWrapper>
        <StyledHeading>User file upload:</StyledHeading>
        <PUpload />
      </StyledUploadWrapper>
      {!isLoading ? (
        <StyledCardListWrapper>
          <StyledCardHeading>
            Here is the preview of the latest uploaded files
          </StyledCardHeading>
          <StyledCardWrapper>
            {data?.splice(0, 5).map((value: any, index: any) => (
              <GridCard
                heading={value?.name}
                description={value?.description}
                image={value?.image}
              />
            ))}
          </StyledCardWrapper>
          <StyledShowMoreButton>
            <Link to="/user-files">
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

export default Home;
