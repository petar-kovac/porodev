import { List, message } from 'antd';
import React, { Dispatch, SetStateAction } from 'react';
import { addFile } from 'service/shared-spaces/shared-spaces';
import styled from 'styled-components';

const SharedSpaceList: React.FC<{
  cardData?: any;
  setIsSiderModalVisible: any;
  data?: unknown[] | undefined;
  setImageParameters?: Dispatch<SetStateAction<string[]>>;
}> = ({
  data,
  setImageParameters = () => undefined,
  cardData,
  setIsSiderModalVisible,
}) => {
  const { id } = cardData;

  return (
    <StyledList
      dataSource={data}
      renderItem={(item: any) => (
        <StyledListItem
          aria-hidden="true"
          onClick={async () => {
            try {
              await addFile({
                sharedSpaceId: item.sharedSpaceId,
                fileId: id,
              });
              setIsSiderModalVisible(false);
            } catch (err: any) {
              console.log(err);
              message.error('File already exists');
            }
          }}
          key={item.sharedSpaceId}
        >
          <List.Item.Meta
            title={item.sharedSpaceName}
            description={item.ownerName}
          />
        </StyledListItem>
      )}
    />
  );
};

const StyledList = styled(List)`
  max-height: 600px;
  overflow-y: auto;
`;

const StyledSelect = styled.div`
  cursor: pointer;
`;
const StyledListItem = styled(List.Item)`
  cursor: pointer;
  /* &:hover,
  &:active,
  &:focus {
    border: 2px solid red;
  } */
`;

const StyledDescription = styled.div`
  display: flex;
  justify-content: space-between;
`;

export default SharedSpaceList;
