import { FC, useEffect } from 'react';
import styled from 'styled-components';

import { PAGES } from 'util/constants/constants';
import {
  StyledPage,
  StyledHeading,
  StyledHeadingWrapper,
} from 'styles/commonStyles';
import PTable from 'components/table/PTable';
import PButton from 'components/buttons/Button';
import theme from 'theme/theme';

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
        <StyledHeading>{PAGES.admins}</StyledHeading>
        <PButton
          text="Add admin"
          color="#000"
          borderRadius="12px"
          background="#fff"
        />
      </StyledHeadingWrapper>
      <PTable dataSource={data} columns={columns} />
    </StyledPage>
  );
};

export default Admins;
