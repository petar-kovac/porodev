import { FC, useEffect } from 'react';
import styled from 'styled-components';
import { Button } from 'antd';

import { pages } from 'constants/constants';
import {
  StyledPage,
  StyledHeading,
  StyledHeadingWrapper,
} from 'styles/commonStyles';
import PTable from 'components/table/PTable';
import useAdminsColumns from './hooks/useAdminsColumns';
import useAdminsData from './hooks/useAdminsData';

const Admins: FC = () => {
  const { columns } = useAdminsColumns();
  const { isLoading, data, error, findData } = useAdminsData();

  useEffect(() => {
    findData();
  }, []);

  return (
    <StyledPage>
      <StyledHeadingWrapper>
        <StyledHeading>{pages.admins}</StyledHeading>
        <Button>aaa</Button>
      </StyledHeadingWrapper>
      <PTable dataSource={data} columns={columns} />
    </StyledPage>
  );
};

const StyledSpinnerWrapper = styled.div`
  display: flex;
  flex: 1;
  justify-content: center;
`;

export default Admins;
