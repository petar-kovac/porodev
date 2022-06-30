import { UserOutlined } from '@ant-design/icons';
import { Avatar, Card } from 'antd';
import useDoubleClick from 'hooks/useDoubleClick';
import React, { FC, RefObject, useRef, MouseEventHandler } from 'react';
import styled from 'styled-components';
import { StyledUserOutlined } from 'styles/icons/styled-icons';
import theme from 'theme/theme';

interface IGroupCardProps {
  groupName?: string;
  isModerator?: boolean;
  moderatorName?: string;
  numberOfFiles?: number;
  numberOfUsers?: number;
  uuid: string;
  selected: boolean;
  onClick?: MouseEventHandler<HTMLElement>;
  onDoubleClick?: MouseEventHandler<HTMLElement>;
}

const GroupCard: FC<IGroupCardProps> = ({
  groupName,
  isModerator,
  moderatorName,
  numberOfFiles,
  numberOfUsers,
  uuid,
  selected,
  onClick,
  onDoubleClick = () => undefined,
}) => {
  const ref = useRef<HTMLDivElement>(null);

  useDoubleClick({ ref, onDoubleClick, onClick, stopPropagation: true });

  return (
    <StyledGroupCard
      ref={ref}
      title={groupName}
      extra={isModerator && <StyledUserOutlined />}
      selected={selected}
    >
      <StyledTextWrapper>
        <div>Moderator:</div>
        <div>{moderatorName}</div>
      </StyledTextWrapper>
      <StyledTextWrapper>
        <div>Number of files:</div>
        <div>{numberOfFiles}</div>
      </StyledTextWrapper>
      <StyledTextWrapper>
        <div>Number of users:</div>
        <div>{numberOfUsers}</div>
      </StyledTextWrapper>
    </StyledGroupCard>
  );
};
const StyledGroupCard = styled(Card).attrs({
  'data-testid': 'group-card',
})<{ selected: boolean; ref: RefObject<HTMLDivElement | null> }>`
  box-shadow: 0 1px #ffffff inset, 1px 3px 8px rgba(34, 25, 25, 0.2);
  height: 25rem;
  border-radius: 1.5rem;
  overflow: hidden;
  width: 24rem;
  cursor: pointer;
  border: 2px solid
    ${({ selected }) => (selected ? `${theme.colors.selected}` : '#fff')};
  background-color: ${({ selected }) =>
    selected ? `${theme.colors.selectedBackground}` : '#fff'};
  &:hover,
  &:active,
  &:focus {
    border: 2px solid
      ${({ selected }) => (selected ? `${theme.colors.selected}` : '#fff')};
  }

  .ant-card-cover {
    height: 14rem;
    width: 24rem;
    overflow: hidden;
    border-top-left-radius: 1.5rem;
    border-top-right-radius: 1.5rem;
  }

  .ant-card-body {
    padding: 1rem 2rem;
  }
`;

const StyledTextWrapper = styled.div`
  display: flex;
  margin-bottom: 14px;
  justify-content: space-between;
`;

export default GroupCard;
