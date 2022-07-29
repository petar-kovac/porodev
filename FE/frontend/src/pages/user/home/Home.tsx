import axios from 'axios';
import { FC, useEffect, useState } from 'react';
import { Link } from 'react-router-dom';

import { Spin } from 'antd';

import GridCard from 'components/cards/grid/GridCard';
import Spinner from 'components/spinner/Spinner';
import PUpload from 'components/upload/PUpload';

import { downloadFile, findFiles } from 'service/files/files';
import PButton from 'components/buttons/PButton';
import theme from 'theme/theme';
import { IFilesCard } from 'types/card-data';
import GridCards from 'components/cards/grid/GridCards';

import { usePageContext } from 'context/PageContext';

import {
  StyledCardHeading,
  StyledCardListWrapper,
  StyledCardWrapper,
  StyledHeading,
  StyledPage,
  StyledShowMoreButton,
  StyledSpinnerWrappper,
  StyledUploadWrapper,
  StyledHomeCard,
} from './home-styled';

const Home: FC = () => {
  const [data, setData] = useState<any>([]);
  const [isPageLoading, setIsPageLoading] = useState<boolean>();

  const { isLoading, setIsLoading } = usePageContext();

  useEffect(() => {
    const fetchFiles = async () => {
      try {
        setIsLoading(true);
        const res = await findFiles();
        setData(res);
      } catch (error) {
        console.log(error);
      }

      setIsLoading(false);
    };

    fetchFiles();
  }, []);

  return (
    <StyledPage>
      <StyledUploadWrapper>
        <StyledHeading>User file upload</StyledHeading>
        <PUpload setFiles={setData} />
      </StyledUploadWrapper>
      {!isPageLoading ? (
        <StyledCardListWrapper>
          <StyledCardHeading>
            Here is the preview of the latest uploaded files
          </StyledCardHeading>
          <StyledCardWrapper>
            {isLoading ? (
              <Spin />
            ) : (
              <>
                {data?.content?.length === 0 ? (
                  <p>No files found</p>
                ) : (
                  <div
                    style={{
                      display: 'flex',
                      flexDirection: 'column',
                      gap: '3rem',
                    }}
                  >
                    <div style={{ display: 'flex', gap: '3rem' }}>
                      {data?.content
                        ?.slice(-4)
                        .reverse()
                        .map((value: any) => (
                          <StyledHomeCard>
                            <GridCard
                              home={false}
                              key={value.fileId}
                              fileName={value.fileName}
                              time={value.uploadDateTime}
                              selected={false}
                              fileExtension={value.fileName.split('.')[1]}
                            />
                          </StyledHomeCard>
                        ))}
                    </div>
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
                  </div>
                )}
              </>
            )}
          </StyledCardWrapper>
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
