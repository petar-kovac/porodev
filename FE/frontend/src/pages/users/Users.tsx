import { FC, useEffect } from 'react';
import { Button } from 'antd';
import styled from 'styled-components';

import {
  StyledHeading,
  StyledHeadingWrapper,
  StyledPage,
} from 'styles/commonStyles';
import { pages } from 'constants/constants';

import PTable from '../../components/table/PTable';
import useUsersColumns from './hooks/useUsersColumns';
import useUsersData from './hooks/useUsersData';

const Users: FC = () => {
  const { columns } = useUsersColumns();
  const { isLoading, data, error, findData } = useUsersData();

  useEffect(() => {
    findData();
  }, []);

  return (
    <StyledPage>
      <StyledHeadingWrapper>
        <StyledHeading>{pages.users}</StyledHeading>
        <Button>aaa</Button>
      </StyledHeadingWrapper>
      <PTable dataSource={data} columns={columns} />
    </StyledPage>
  );
};

export default Users;
