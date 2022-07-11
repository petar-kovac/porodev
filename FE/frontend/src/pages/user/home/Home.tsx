import axios from 'axios';
import { FC, useEffect, useState } from 'react';
import { Link } from 'react-router-dom';

import GridCard from 'components/card/GridCard';
import Spinner from 'components/spinner/Spinner';
import PUpload from 'components/upload/PUpload';

import { downloadFile, findFiles } from 'service/files/files';
import PButton from 'components/buttons/PButton';
import theme from 'theme/theme';
import { IFilesCard } from 'types/card-data';
import GridCards from 'components/card/GridCards';
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
  const [data, setData] = useState<any>(undefined);
  const [isLoading, setIsLoading] = useState<boolean>();

  useEffect(() => {
    const fetchFiles = async () => {
      const res = await findFiles();
      setData(res);
    };

    fetchFiles();
  }, []);

  console.log(data);

  return (
    <StyledPage>
      <StyledUploadWrapper>
        <StyledHeading>User file upload:</StyledHeading>
        <PUpload setFiles={setData} />
      </StyledUploadWrapper>
      {!isLoading ? (
        <StyledCardListWrapper>
          <StyledCardHeading>
            Here is the preview of the latest uploaded files
          </StyledCardHeading>
          <StyledCardWrapper>
            {data?.content
              .slice(0, 4)
              .reverse()
              .map((value: any) => (
                <GridCard
                  key={value.fileId}
                  heading={value.fileName}
                  description={value.uploadTime}
                  selected={false}
                  fileExtension={value.fileName.split('.')[1]}
                />
              ))}
          </StyledCardWrapper>
          <StyledShowMoreButton>
            <Link to="/user-files">
              <PButton
                text="Show more files"
                color="#fff"
                radius="12px"
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
