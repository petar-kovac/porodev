import axios from 'axios';
import { FC, useEffect, useState } from 'react';
import styled from 'styled-components';
import { boolean } from 'yup';
import PCard from '../../components/card/PCard';
import { findFiles } from '../../service/files/files';

const Files: FC = () => {
  const [data, setData] = useState<[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(false);

  useEffect(() => {
    const fetchFiles = async () => {
      try {
        await axios
          .get(`${process.env.REACT_APP_MOCK_URL}/files`)
          .then((res) => setData(res.data));
      } catch (err) {
        console.log(err);
      }
    };
    fetchFiles();
  }, []);

  return (
    <StyledFilesWrapper>
      {data?.map((value: any, index: any) => (
        <PCard
          heading={value?.name}
          description={value?.description}
          image={value?.image}
        />
      ))}
    </StyledFilesWrapper>
  );
};
const StyledFilesWrapper = styled.div`
  padding: 20px;
  gap: 10px;
  display: flex;
  flex-wrap: wrap;
`;

export default Files;
