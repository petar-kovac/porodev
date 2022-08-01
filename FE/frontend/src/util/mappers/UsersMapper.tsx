import { CloseOutlined, UserOutlined } from '@ant-design/icons';
import { Input } from 'antd';
import { useGroupsContext } from 'context/GroupsContext';
import { usePageContext } from 'context/PageContext';
import { FC, useEffect, useState } from 'react';
import { getAllUsersFromSharedSpace } from 'service/shared-spaces/shared-spaces';
import styled from 'styled-components';

/**
 * Component to set input fields in a Runtime sider.
 */
const UsersMapper: FC<{
  selectedCardId: number | null | undefined;
}> = ({ selectedCardId }) => {
  const { inputParameters, dispatchInput } = useGroupsContext();
  const [userList, setUserList] = useState<any>(undefined);
  const { setSharedSpaceId, sharedSpaceId, userTrigger } = usePageContext();

  useEffect(() => {
    const fetch = async () => {
      if (selectedCardId !== null) {
        const res = await getAllUsersFromSharedSpace(selectedCardId);
        setUserList(res);
      }
    };
    fetch();
  }, [selectedCardId, userTrigger]);

  return (
    <>
      {userList?.map((value: any, index: number) => {
        return (
          <StyledInputRow>
            <StyledField>
              <StyledTextField>
                <UserOutlined />
                <div>{value.name}</div>
                <div>{value.lastname}</div>
              </StyledTextField>
            </StyledField>
            {/* <StyledInput
              value={value as unknown as string}
              placeholder="Add parameter"
              onChange={(e) => {
                dispatchInput({
                  type: 'CHANGE_INPUT_FIELD_VALUE',
                  payload: { e, index },
                });
              }}
            />
            <StyledClose
              onClick={() => {
                dispatchInput({
                  type: 'DELETE_SINGLE_FIELD',
                  payload: { index },
                });
              }}
            /> */}
          </StyledInputRow>
        );
      })}
    </>
  );
};

const StyledTextField = styled.div`
  display: flex;
  gap: 10px;
  align-items: center;
`;

const StyledField = styled.div`
  display: flex;
  width: 260px;
  height: 32px;
  border-radius: 10px;
  border: 1px solid lightgray;
  justify-content: space-between;
  padding: 0 8px;
  align-items: center;
`;

const StyledInputRow = styled.div`
  display: flex;
  position: relative;
  align-items: center;
  width: 260px;
`;
const StyledInput = styled(Input)`
  border-radius: 12px;
  width: 260px;
`;
const StyledClose = styled(CloseOutlined)`
  position: absolute;
  color: red;
  cursor: pointer;
  left: 235px;
`;

export default UsersMapper;
