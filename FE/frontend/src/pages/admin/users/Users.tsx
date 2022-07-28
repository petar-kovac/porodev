import { FC, useEffect } from 'react';

import PButton from 'components/buttons/PButton';
import PTable from 'components/table/PTable';
import { PAGES } from 'util/constants/constants';
import {
  StyledHeading,
  StyledHeadingWrapper,
  StyledPage,
} from './users-styled';

import useUsersColumns from './hooks/useUsersColumns';
import useUsersData from './hooks/useUsersData';

const Users: FC = () => {
  const { columns } = useUsersColumns();
  const { isLoading, data, error } = useUsersData();

  return (
    <StyledPage>
      <StyledHeadingWrapper>
        <StyledHeading>{PAGES.users}</StyledHeading>
        <PButton text="Add user" color="#000" radius="12px" background="#fff" />
      </StyledHeadingWrapper>
      <PTable dataSource={data} columns={columns} />
    </StyledPage>
  );
};

export default Users;
